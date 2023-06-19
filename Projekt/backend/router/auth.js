const router = require("express").Router();
const { User } = require("../models/user");
const bcrypt = require("bcrypt");
const jwt = require("jsonwebtoken");
const Joi = require("joi");

const validate = (data) => {
  const schema = Joi.object({
    email: Joi.string().email().required().label("Email"),
    password: Joi.string().required().label("Password"),
  });
  return schema.validate(data);
};

// Endpoint logowania użytkownika
router.post("/", async (req, res) => {
  try {
    const { error } = validate(req.body);
    if (error) {
      return res.status(400).send({ message: error.details[0].message });
    }

    const { email, password } = req.body;

    // Sprawdzenie, czy użytkownik o podanym emailu istnieje
    const user = await User.findOne({ email });
    if (!user) {
      return res.status(401).send({ message: "Invalid Email or Password" });
    }

    // Porównanie hasła
    const validPassword = await bcrypt.compare(password, user.password);
    if (!validPassword) {
      return res.status(401).send({ message: "Invalid Email or Password" });
    }

    // Sprawdzenie, czy użytkownik ma rolę admina
    const isAdmin = user.role === 'admin';

    // Generowanie tokena uwierzytelniającego
    const token = jwt.sign({ _id: user._id, isAdmin }, process.env.JWTPRIVATEKEY, {
      expiresIn: "7d",
    });

    res.status(200).send({ data: token, message: "Logged in successfully" });
  } catch (error) {
    res.status(500).send({ message: "Internal Server Error" });
  }
});

module.exports = router;
