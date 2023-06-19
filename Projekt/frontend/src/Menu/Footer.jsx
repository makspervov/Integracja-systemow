import React from 'react'
import '../styles/footer/footer.css'
const Footer = () => {

  const year = new Date().getFullYear()
  return <footer className="footer">
  <div class="footer_content">
    <p class="footer_copyright">CO2 &copy; 2023</p>
  </div>
</footer>

}

export default Footer