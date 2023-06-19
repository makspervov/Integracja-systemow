const express = require('express');
const connectDB = require('./database');
const cors = require('cors');
const co2EmissionByCountryRoutes = require('./router/co2EmmisionbyCountry');
const co2EmissionBySectorRoutes = require('./router/co2EmmisionbySector');
const forestAreaByCountryRoutes = require('./router/forestAreabyCountry');
const landAreaRoutes = require('./router/landArea');
const User = require('./router/users');
const Auth = require('./router/auth');
const getUserRouter = require('./router/getUser');
const deleteUserRouter = require('./router/deleteUser');
const app = express();

// Połącz z bazą danych
connectDB();

app.use(cors({
  origin: 'http://localhost:3001',
}));
// Middleware do parsowania ciała żądań jako JSON
app.use(express.json());


// Endpointy API dla rejestracji i logowania użytkowników
app.use('/api/users', User);
app.use('/api/auth', Auth);
app.use('/api/getUser', getUserRouter);
app.use('/api/deleteUser', deleteUserRouter);


// Endpointy API dla kolekcji "CO2EmissionByCountry"
app.use('/api', co2EmissionByCountryRoutes);

// Endpointy API dla kolekcji "CO2EmissionBySector"
app.use('/api', co2EmissionBySectorRoutes);

// Endpointy API dla kolekcji "ForestAreaByCountry"
app.use('/api', forestAreaByCountryRoutes);

// Endpointy API dla kolekcji "LandArea"
app.use('/api', landAreaRoutes);



// Uruchom serwer
const PORT = 3000;
app.listen(PORT, () => {
  console.log(`Serwer działa na porcie ${PORT}`);
});
