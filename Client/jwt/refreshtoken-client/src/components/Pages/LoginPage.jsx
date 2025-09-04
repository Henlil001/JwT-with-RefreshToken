import { useState, useContext } from "react";
import { NavLink, useNavigate, useParams } from "react-router-dom";
import eyeOff from "../../assets/images/eyeOff.png";
import eye from "../../assets/images/eye.png";
import LoginHeader from "../../components/Headers/LoginHeader";
import { AuthContext } from "../../context/AuthProvider";
import Spinner from "../Features/Spinner";

const LoginPage = () => {
  const {
    handleLogin,
    loading,
  } = useContext(AuthContext);
  const [showPassword, setShowPassword] = useState(false); // State för att hantera visning av lösenord
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState(false);

  const togglePasswordVisibility = () => {
    setShowPassword(!showPassword); // Växla mellan att visa och dölja lösenordet
  };

  const handleSubmit = async (event) => {
    // Förhindra standardformulärs submission
    event.preventDefault();
    const form = event.currentTarget;

    if (!form.checkValidity()) event.stopPropagation();

    if (email === "" || password === "") return;

    const result = await handleLogin({
      email: email,
      password: password,
    });
    debugger;
    if(result === false){
      setError(true);
    }
  };

  return (
    <>
      <LoginHeader />
      
      <div className="container position-relative">
        
        {/* Spinner-overlay */}
        {loading && (
          <div
            className="position-absolute top-0 start-0 w-100 h-100 d-flex justify-content-center align-items-center"
            style={{ backgroundColor: "rgba(255, 255, 255, 0.7)", zIndex: 10 }}
          >
            <Spinner />
          </div>
        )}
        <div className="row justify-content-center">
          {/* Anpassa layouten för olika skärmstorlekar */}
          <div className="col-lg-4 col-md-6 col-sm-8 col-12 mt-5 border rounded p-4 bg-body-tertiary">
            <form onSubmit={handleSubmit}>
              <div className="mb-3">
                <label htmlFor="exampleInputEmail1" className="form-label">
                  Email
                </label>
                <input
                  type="email"
                  className="form-control"
                  id="exampleInputEmail1"
                  onChange={(e) => setEmail(e.target.value)}
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
                 {/* Felmeddelande under formuläret */}
              {error && (
                <div
                  className="alert alert-danger alert-dismissible fade show mt-3"
                  role="alert"
                >
                  Felaktig inloggning, Kontrollera email och lösenord Eller har du glömt ditt lösenord.
                  <button
                    type="button"
                    className="btn-close"
                    aria-label="Close"
                    onClick={() => setError(false)}
                  ></button>
                </div>
              )}

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
