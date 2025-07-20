import { NavLink } from "react-router-dom";
import klubbaloga from "../../assets/images/hockeyklubbaloga.jpg";
import './css/LoginHeader.css'

const LoginHeader = () => {
  return (
    <nav className="navbar bg-body-tertiary position-relative border">
            <div className="container-fluid d-flex align-items-center">
        {/* Logo till vänster */}
        <NavLink className="navlink" to="/">
          <img
            src={klubbaloga}
            alt="Logo"
            width="60"
            height="54"
            className="d-inline-block align-text-top border rounded"
          />
        </NavLink>
      </div>
      {/* Centrera rubriken */}
      <div className="position-absolute top-50 start-50 translate-middle">
        <NavLink className="navlink" to="/">
          <h1>Home</h1>
        </NavLink>
      </div>
    </nav>
  );
};

export default LoginHeader;