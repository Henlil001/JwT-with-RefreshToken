
import { useState, useContext } from "react";
import { NavLink } from "react-router-dom";
import AccountIcon from "../../assets/images/accountIcon.png";
import { AuthContext } from "../../context/AuthProvider";
import './css/HomeHeader.css'

const Header = ({ imageUrl, title }) => {
  const { LogOut } = useContext(AuthContext);
  const [dropdownOpen, setDropdownOpen] = useState(false);

  // Toggle funktion för att öppna/stänga dropdown
  const toggleDropdown = () => {
    setDropdownOpen(!dropdownOpen);
  };

  return (
    <>
      <nav className="navbar navbar-expand-lg navbar-light bg-body-tertiary position-relative border">
        <div className="container-fluid d-flex align-items-center">
          {/* Logo till vänster */}
          <NavLink className="navlink" to="/home">
            <img
              src={imageUrl ? imageUrl : klubbaloga}
              alt="Logo"
              width="60"
              height="54"
              className="d-inline-block align-text-top border rounded"
            />
          </NavLink>
        </div>

        {/* Kategorier eller titel (om behövs) */}
        <div className="position-absolute top-50 start-50 translate-middle w-100">
          <ul className="nav d-none d-lg-flex justify-content-center w-100">
            <li className="nav-item mx-4">
              <NavLink
                className="nav-link text-secondary-emphasis fs-3"
                to="/istraning"
              >
                Isträning
              </NavLink>
            </li>

            <li className="nav-item mx-4">
              <NavLink
                className="nav-link text-secondary-emphasis fs-3"
                to="/malvakt"
              >
                Målvakt
              </NavLink>
            </li>

            <li className="nav-item mx-4">
              <NavLink
                className="nav-link text-secondary-emphasis fs-3"
                to="/fys"
              >
                Fysträning
              </NavLink>
            </li>

            <li className="nav-item mx-4">
              <NavLink
                className="nav-link text-secondary-emphasis fs-3"
                to="/filosofi"
              >
                Träningsfilosofi
              </NavLink>
            </li>
          </ul>
        </div>

        <div className="d-flex ms-auto position-absolute top-50 end-0 translate-middle-y">
          <button
            className="btn btn-link"
            onClick={toggleDropdown}
            aria-expanded={dropdownOpen ? "true" : "false"}
          >
            <img
              src={AccountIcon}
              alt="Konto Ikon"
              width="60"
              height="54"
              className="d-inline-block align-text-top border rounded"
            />
          </button>

          {/* Dropdown meny */}
          {dropdownOpen && (
            <div
              className="dropdown-menu show text-center"
              style={{
                position: "absolute",
                top: "100%",
                right: "0",
                minWidth: "250px",
                zIndex: "1050",
                maxHeight: "80vh", // Begränsa höjden så att den inte går utanför skärmen
                overflowY: "auto", // Lägg till rullning om det behövs
                width: "auto", // Låter bredden vara dynamisk
              }}
            >
              {/* Kategorier som dyker upp vid små skärmar */}
              <div className="d-lg-none">
                <NavLink className="dropdown-item" to="/istraning">
                  Isträning
                </NavLink>
                <div className="dropdown-divider"></div>
                <NavLink className="dropdown-item" to="/malvakt">
                  Målvakt
                </NavLink>
                <div className="dropdown-divider"></div>
                <NavLink className="dropdown-item" to="/fys">
                  Fysträning
                </NavLink>
                <div className="dropdown-divider"></div>
                <NavLink className="dropdown-item" to="/filosofi">
                  Träningsfilosofi
                </NavLink>
                <div className="dropdown-divider"></div>
              </div>
              {/* Alltid synliga länkar */}
              <NavLink className="dropdown-item" to="/profile">
                Hantera profil
              </NavLink>
              <div className="dropdown-divider"></div>
              <button onClick={()=>LogOut()} className="dropdown-item">
                Logga ut
              </button>
            </div>
          )}
        </div>
      </nav>
    </>
  );
};

export default Header;
