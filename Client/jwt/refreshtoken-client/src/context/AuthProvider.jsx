import { useState, createContext, useEffect } from "react";
import Cookies from "js-cookie";
import jwtDecode from "jwt-decode";
import { useNavigate, useParams } from "react-router-dom";
import * as AuthService from "../services/AuthService";

export const AuthContext = createContext();

const AuthProvider = (props) => {
  const navigate = useNavigate();
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [loading, setLoading] = useState(false);
  const [user, setUser] = useState(null);
  const [emailAllreadyExist, setEmailAllreadyExist] = useState(null)

  const setTokenAndUser = (token) => {
    localStorage.setItem("accesstoken", token);
    if (token) {
      debugger
      const decoded = jwtDecode(token);
      setUser({
        id: decoded.id,
        username: decoded.username,
      });
      setIsAuthenticated(true);
    } else {
      setUser(null);
      setIsAuthenticated(false);
    }
  };

  const LogOut = () => {
    setIsAuthenticated(false);
    localStorage.removeItem("accesstoken");
    navigate("/");
  };

  const handleLogin = async ({ epost, password }) => {
    setLoading(true)
    try {
      const response = await AuthService.Login(epost, password);
      setTokenAndUser(data.accessToken);
      if (response.status === 200) {
        const data = response.json();
        setTokenAndUser(data.accessToken);
        navigate("/home");
      } else if (response.status === 401) {
        isAuthenticated(false);
      }
      setLoading(false);
    } catch (error) {
      console.error("Login failed", error);
      navigate("/error");
      setLoading(false);
    }
  };

  const handleRegisterUser = async (newUser) => {
    setLoading(true);
    try {
      const response = await AuthService.CreateUser({newUser})
        if (response.status === 200) {
        const data = response.json();
        setTokenAndUser(data.accessToken);
        navigate("/home");
      } else if (response.status === 409) {
        setIsAuthenticated(false);
        setEmailAllreadyExist(true);
      }
      
    } catch (error) {
      console.error("register new user failed", error);
      navigate("/error");
    }
    setLoading(false);
  };

  const handleRefreshtoken = async () => {
    setLoading(true);
    try {
      const response = await AuthService.RefreshToken();
      if (response.status === 401) {
        LogOut();
      } else if (response === 200) {
        const data = response.json();
        setTokenAndUser(data.accessToken)
      }
    } catch (error) {
      console.error("refreshtoken failed", error);
      navigate("/error");
    }
     setLoading(false);
  };

  return (
    <AuthContext.Provider
      value={{
        handleLogin,
        handleRegisterUser,
        handleRefreshtoken,
        LogOut,
        isAuthenticated,
        loading,
        user,
        emailAllreadyExist
      }}
    >
      {props.children}
    </AuthContext.Provider>
  );
};

export default AuthProvider;
