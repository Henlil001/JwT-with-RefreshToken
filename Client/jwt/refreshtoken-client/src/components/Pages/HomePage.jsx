import Header from "../../components/Headers/HomeHeader";
import { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../context/AuthProvider";

const HomePage = () => {
  const { handleRefreshtoken, LogOut, isAuthenticated, loading, user } =
    useContext(AuthContext);
  const navigate = useNavigate();

  useEffect(() => {
    if(isAuthenticated === false){
      navigate("/");
    }
  }, [isAuthenticated]);

  return (
    <>
      <Header title="Övningsbank" />
      <div className="text-align-center">
        <h1>Startsida</h1>
        <p>
          Hej och välkommen <strong>{user.name}</strong>
        </p>
      </div>
    </>
  );
};

export default HomePage;
