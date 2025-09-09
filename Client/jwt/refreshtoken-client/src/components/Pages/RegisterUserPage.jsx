import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import LoginHeader from "../../components/Headers/LoginHeader/";
import eyeOff from "../../assets/images/eyeOff.png";
import eye from "../../assets/images/eye.png";
import { AuthContext } from "../../context/AuthProvider";
import { useContext } from "react";
import Spinner from "../Features/Spinner";

const RegisterUser = () => {
  const navigate = useNavigate();
  const { handleRegisterUser, loading } = useContext(AuthContext);
  

  const [showFirstPassword, setShowFirstPassword] = useState(false);

  const [epost, setEpost] = useState("");
  const [repeatEpost, setRepeatEpost] = useState("");
  const [password, setPassword] = useState("");
  const [firstname, setFirstname] = useState("");
  const [lastname, setlastname] = useState("");
  const [error, setError] = useState(false)

  const [checkEpost, setCheckEpost] = useState(true);

  const toggleFirstPasswordVisibility = () => {
    setShowFirstPassword(!showFirstPassword);
  };

  useEffect(() => {
    setCheckEpost(epost === repeatEpost);
  }, [epost, repeatEpost]);

  const handleSubmit = async (event) => {
    event.preventDefault();
    const form = event.currentTarget;

    if (!checkEpost) {
      alert("Epost matchar inte.");
      return;
    }

    if (form.checkValidity() === false) {
      event.stopPropagation();
      form.classList.add("was-validated");
      return;
    }

    const result = await handleRegisterUser({
      email: epost,
      password: password,
      firstname: firstname,
      lastname: lastname,
    });

    if(!result){
      setError(true)
    }
    else{
      navigate("/home");
    }
  };

  return (
    <>
      <LoginHeader />
      <br />
      <br />

      <div className="container-fluid d-flex flex-column pt-3 align-items-center vh-100">
        <form
          className="row g-3 needs-validation w-100 bg-body-tertiary border rounded"
          style={{ maxWidth: "400px" }}
          noValidate
          onSubmit={handleSubmit}
        >
          {/* Spinner-overlay */}
        {loading && (
          <div
            className="position-absolute top-0 start-0 w-100 h-100 d-flex justify-content-center align-items-center"
            style={{ backgroundColor: "rgba(255, 255, 255, 0.7)", zIndex: 10 }}
          >
            <Spinner />
          </div>
        )}
          {/* Förnamn */}
          <div className="col-12 position-relative">
            <label htmlFor="fornamn" className="form-label ms-1">
              Förnamn
            </label>
            <input
              type="text"
              className="form-control"
              id="fornamn"
              onChange={(e) => setFirstname(e.target.value)}
              required
            />
          </div>

          {/* Efternamn */}
          <div className="col-12 position-relative">
            <label htmlFor="lastname" className="form-label ms-1">
              Efternamn
            </label>
            <input
              type="text"
              className="form-control"
              id="lastname"
              onChange={(e) => setlastname(e.target.value)}
              required
            />
          </div>

          {/* Epost */}
          <div className="col-12 position-relative">
            <label htmlFor="epost1" className="form-label ms-1">
              Epost
            </label>
            <input
              type="email"
              className="form-control"
              id="epost1"
              onChange={(e) => setEpost(e.target.value)}
              required
            />
          </div>

          <div className="col-12 position-relative">
            <label htmlFor="epost2" className="form-label ms-1">
              Upprepa Epost
            </label>
            <input
              type="email"
              className={`form-control ${checkEpost ? "" : "is-invalid"}`}
              id="epost2"
              onChange={(e) => setRepeatEpost(e.target.value)}
              required
            />
            {!checkEpost && (
              <div className="invalid-feedback">
                Epostadresserna matchar inte.
              </div>
            )}
          </div>

          {/* Lösenord */}
          <div className="col-12 position-relative">
            <label htmlFor="Password1" className="form-label ms-1">
              Lösenord
            </label>
            <div className="input-group has-validation">
              <input
                type={showFirstPassword ? "text" : "password"}
                className="form-control"
                id="Password1"
                onChange={(e) => setPassword(e.target.value)}
                required
              />
              <button
                className="btn btn-sm border-0"
                type="button"
                onClick={toggleFirstPasswordVisibility}
              >
                <img
                  src={showFirstPassword ? eye : eyeOff}
                  alt={showFirstPassword ? "Dölj" : "Visa"}
                  style={{ width: "20px", height: "20px" }}
                />
              </button>
            </div>
          </div>
 {/* Felmeddelande under formuläret */}
              {error && (
                <div
                  className="alert alert-danger alert-dismissible fade show mt-3"
                  role="alert"
                >
                  Eposten är redan registrerad, har du glömt ditt lösenord?.
                  <button
                    type="button"
                    className="btn-close"
                    aria-label="Close"
                    onClick={() => setError(false)}
                  ></button>
                </div>
              )}
          <div className="col-12 pb-2 text-center">
            <button
              className="btn btn-primary mb-4 mt-4 btn-lg w-50"
              type="submit"
            >
              Skapa konto
            </button>
          </div>
        </form>
      </div>
    </>
  );
};

export default RegisterUser;
