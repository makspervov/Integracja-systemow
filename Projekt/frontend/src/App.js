import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Header from './Menu/Header';
import Home from './Menu/Home';
import CountryTable from './Tables/countryTable';
import Diagram from './Diagrams/Diagram';
import Footer from './Menu/Footer';
import RegisterPage from './User/RegisterUser';
import LoginPage from './User/loginUser';
import AdminPage from './User/AdminPage';
const App = () => {
  return (
    <div className="App">
      <Header />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/countryTable" element={<CountryTable />} />
        <Route path="/diagram" element={<Diagram />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/admin" element={<AdminPage />} />
      </Routes>
      <Footer />
    </div>
  );
};

export default App;
