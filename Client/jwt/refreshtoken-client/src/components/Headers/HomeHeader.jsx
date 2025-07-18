import { NavLink } from "react-router-dom";
import "./LoginHeader.css";

const LoginHeader = () => {
  return (
    <nav className="navbar bg-body-tertiary position-relative border">
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