import React from 'react';
import styles from '../styles/header/styles.module.css';
import { NavLink } from 'react-router-dom';

const Header = () => {
  const isAuthenticated = localStorage.getItem('token');


  const handleLogout = () => {
    localStorage.removeItem('token');
    window.location.reload();
  };

  const goToDiagram = () => {
    window.location = '/diagram';
  };

  const goToHome = () => {
    window.location = '/';
  }

  return (
    <div className={styles.main_container}>
      <nav className={styles.navbar}>
        <h1 onClick={goToHome}>CO2</h1>
        <div>
          <button className={styles.white_btn} onClick={goToDiagram}>
            Go to diagram
          </button>
          {isAuthenticated ? (
            <>
              <button className={styles.white_btn} onClick={handleLogout}>
                Logout
              </button>
            </>
          ) : (
            <>
              <NavLink to="/login">
                <button className={styles.white_btn}>Login</button>
              </NavLink>
              <NavLink to="/register">
                <button className={styles.white_btn}>Sign up</button>
              </NavLink>
            </>
          )}
        </div>
      </nav>
    </div>
  );
};

export default Header;
