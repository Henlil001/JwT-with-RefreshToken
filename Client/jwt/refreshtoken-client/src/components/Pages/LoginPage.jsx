import { useState, useContext } from "react";
import { NavLink, useNavigate, useParams } from "react-router-dom";
import eyeOff from "../../assets/images/eyeOff.png";
import eye from "../../assets/images/eye.png";
import LoginHeader from "../../components/Headers/LoginHeader";
import { AuthContext } from "../../context/AuthProvider";

const LoginPage = () => {
  const { handleLogin } = useContext(AuthContext);
  const [showPassword, setShowPassword] = useState(false); // State för att hantera visning av lösenord
  const [epost, setEpost] = useState("");
  const [password, setPassword] = useState("");

  const togglePasswordVisibility = () => {
    setShowPassword(!showPassword); // Växla mellan att visa och dölja lösenordet
  };

  const handleSubmit = (event) => {
    // Förhindra standardformulärs submission
    event.preventDefault();
    const form = event.currentTarget;

    if (!form.checkValidity()) event.stopPropagation();

    if (epost === "" || password === "") return;

    handleLogin({
      userName: epost,
      password: password,
    });
  };

  return (
    <>
      <LoginHeader />

      <div className="container">
        <div className="row justify-content-center">
          {/* Anpassa layouten för olika skärmstorlekar */}
          <div className="col-lg-4 col-md-6 col-sm-8 col-12 mt-5 border rounded p-4 bg-body-tertiary">
            <form onSubmit={handleSubmit}>
              <div className="mb-3">
                <label htmlFor="exampleInputEmail1" className="form-label">
                  Epost
                </label>
                <input
                  type="email"
                  className="form-control"
                  id="exampleInputEmail1"
                  onChange={(e) => setEpost(e.target.value)}
                  aria-describedby="emailHelp"
                />
              </div>
              <div className="mb-5">
                <label htmlFor="exampleInputPassword1" className="form-label">
                  Lösenord
                </label>
                <div className="input-group">
                  <input
                    type={showPassword ? "text" : "password"}
                    className="form-control rounded 2"
                    id="exampleInputPassword1"
                    onChange={(e) => setPassword(e.target.value)}
                  />
                  <button
                    className="btn btn-sm rounded 2 border-0"
                    type="button"
                    onClick={togglePasswordVisibility}
                  >
                    <img
                      src={showPassword ? eye : eyeOff} //namnen blev tokiga men de e rätt
                      alt={showPassword ? "Dölj" : "Visa"}
                      style={{ width: "20px", height: "20px" }} // Justera storlek på ikonerna
                    />
                  </button>
                </div>
              </div>
              <div className="text-center">
                <button type="submit" className="btn btn-primary btn-lg w-100">
                  Logga in
                </button>
              </div>
              <div className="text-center">
                <br />
                <div className="d-flex justify-content-center gap-4">
                  <NavLink to="register/user">
                    <small>Skapa konto</small>
                  </NavLink>
                  <a href="">
                    <small>Glömt lösenord</small>
                  </a>
                </div>
              </div>
            </form>
          </div>
        </div>
      </div>
    </>
  );
};

export default LoginPage;
