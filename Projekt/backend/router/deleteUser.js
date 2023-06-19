const router = require("express").Router();
const { User } = require("../models/user");

router.delete("/:userId", async (req, res) => {
  const { userId } = req.params;
  try {
    await User.findByIdAndDelete(userId);
    res.json({ message: "User deleted successfully." });
  } catch (error) {
    res.status(500).json({ message: "Failed to delete user." });
  }
});

module.exports = router;
