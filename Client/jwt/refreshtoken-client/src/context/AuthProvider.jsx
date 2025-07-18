import { useState, createContext, useEffect } from "react";
import Cookies from "js-cookie";
import { useNavigate, useParams } from "react-router-dom";

export const AuthContext = createContext();

const AuthProvider = (props) => {
  const navigate = useNavigate();
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [loading, setLoading] = useState(true);

  const LogOut = () => {

    navigate("/");
  };
  const Login = (accessToken, refreshToken) => {

  };

  const handleLogin = async (loginVal) => {
    debugger;
   
  };

  const handleRegisterUser = async (newUser) => {
   
  };

  return (
    <AuthContext.Provider
      value={{
        handleLogin,
        handleRegisterUser,
        LogOut,
        isAuthenticated,
        loading,
      }}
    >
      {props.children}
    </AuthContext.Provider>
  );
};

export default AuthProvider;
