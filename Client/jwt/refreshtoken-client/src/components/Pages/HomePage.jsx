import { jwtDecode } from "jwt-decode";
import Header from "../../components/Headers/HomeHeader";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

const HomePage = () => {
  const [username, setUsername] = useState("");
    const navigate = useNavigate(); // ✅ måste ligga här

  useEffect(() => {
    debugger;
    // 1. Hämta JWT från localStorage (eller cookie om du använder det)
    const token = localStorage.getItem("accessToken"); // justera om du använder annat namn

    if (token) {
      try {
        // 2. Avkoda token
        const decoded = jwtDecode(token);

        // 3. Sätt värde från t.ex. "name" eller "sub"
        setUsername(decoded.name);
      } catch (error) {
        console.error("Ogiltig token", error);
      }
    }
    else{

      navigate('/')
    }
  }, []);

  return (
    <>
      <Header title="Övningsbank" />
      <div className="text-align-center">
        <h1>Startsida</h1>
        <p>
          Du är inloggad som <strong>{username}</strong>
        </p>
      </div>
    </>
  );
};

export default HomePage;
