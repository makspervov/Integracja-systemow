require('dotenv').config();
const mongoose = require('mongoose');

const connectDB = async () => {
  try {
    // Połącz z bazą danych MongoDB
    mongoose.connect('mongodb://mongo:27017/CO2', {
      useNewUrlParser: true,
      useUnifiedTopology: true,
    });

    console.log('Połączono z bazą danych MongoDB');
  } catch (error) {
    console.error('Błąd połączenia z bazą danych MongoDB', error);
    process.exit(1); // Zakończ aplikację w przypadku błędu połączenia
  }
};

module.exports = connectDB;
