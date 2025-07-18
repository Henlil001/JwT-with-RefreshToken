import { useState, useEffect } from "react";
import LoginHeader from "../../components/Headers/LoginHeader/LoginHeader";
import eyeOff from "../../assets/images/eyeOff.png";
import eye from "../../assets/images/eye.png";
import { AppContext } from "../../context/AppProvider";
import { AuthContext } from "../../context/AuthProvider";
import { useContext, useRef } from "react";


const RegisterUser = () => {
  const { allClubs } = useContext(AppContext);
  const { handleRegesterUser } = useContext(AuthContext);

  const [showFirstPassword, setShowFirstPassword] = useState(false);
  const [showSecondPassword, setShowSecondPassword] = useState(false);

  const [epost, setEpost] = useState("");
  const [repeatEpost, setRepeatEpost] = useState("");
  const [password, setPassword] = useState("");
  const [repeatPassword, setRepeatPassword] = useState("");
  const [username, setUsername] = useState("");
  const [club, setClub] = useState("");

  const [checkEpost, setCheckEpost] = useState(true);
  const [checkPassword, setCheckPassword] = useState(true);

  const toggleFirstPasswordVisibility = () => {
    setShowFirstPassword(!showFirstPassword);
  };

  const toggleSecondPasswordVisibility = () => {
    setShowSecondPassword(!showSecondPassword);
  };

  useEffect(() => {
    setCheckEpost(epost === repeatEpost);
  }, [epost, repeatEpost]);

  useEffect(() => {
    setCheckPassword(password === repeatPassword);
  }, [password, repeatPassword]);

  const handleSubmit = (event) => {
    event.preventDefault();
    const form = event.currentTarget;

    if (!checkEpost || !checkPassword) {
      alert("Epost eller lösenord matchar inte.");
      return;
    }

    if (form.checkValidity() === false) {
      event.stopPropagation();
      form.classList.add("was-validated");
      return;
    }

    handleRegesterUser({
      email: epost,
      username: username,
      password: password,
      myClub: club,
    });
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
          {/* Användarnamn */}
          <div className="col-12 position-relative">
            <label htmlFor="username" className="form-label ms-1">
              Användarnamn
            </label>
            <input
              type="text"
              className="form-control"
              id="username"
              onChange={(e) => setUsername(e.target.value)}
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

          <div className="col-12 position-relative">
            <label htmlFor="Password2" className="form-label ms-1">
              Upprepa Lösenord
            </label>
            <div className="input-group has-validation">
              <input
                type={showSecondPassword ? "text" : "password"}
                className={`form-control ${checkPassword ? "" : "is-invalid"}`}
                id="Password2"
                onChange={(e) => setRepeatPassword(e.target.value)}
                required
              />
              <button
                className="btn btn-sm border-0"
                type="button"
                onClick={toggleSecondPasswordVisibility}
              >
                <img
                  src={showSecondPassword ? eye : eyeOff}
                  alt={showSecondPassword ? "Dölj" : "Visa"}
                  style={{ width: "20px", height: "20px" }}
                />
              </button>
              {!checkPassword && (
                <div className="invalid-feedback">Lösenord matchar inte.</div>
              )}
            </div>
          </div>

          {/* Välj klubb */}
          <div className="col-12 position-relative">
            <label htmlFor="club" className="form-label ms-1">
              Klubb
            </label>
            <select
              className="form-select "
              id="club"
              onChange={(e) => setClub(e.target.value)}
              required
            >
              <option value="">Välj...</option>
              {allClubs.map((club) => (
                <option key={club.clubID} value={club.clubName}>
                  {club.clubName}
                </option>
              ))}
            </select>
            <div className="invalid-feedback">Välj klubb.</div>
          </div>

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