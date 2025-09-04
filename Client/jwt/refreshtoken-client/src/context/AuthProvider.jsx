import { useState, createContext, useEffect } from "react";
import { jwtDecode } from "jwt-decode";
import { useNavigate, useParams } from "react-router-dom";
import * as AuthService from "../services/AuthService";

export const AuthContext = createContext();

const AuthProvider = (props) => {
  const navigate = useNavigate();
  const [isAuthenticated, setIsAuthenticated] = useState(null);
  const [loading, setLoading] = useState(false);
  const [user, setUser] = useState({});
  const [emailAllreadyExist, setEmailAllreadyExist] = useState(null);

  const setTokenAndUser = (token) => {
    localStorage.setItem("accesstoken", token);
    if (token) {
      debugger;
      const decoded = jwtDecode(token);
      setUser({
        id: decoded.id,
        name: decoded.name,
      });
      setIsAuthenticated(true);
    } else {
      setUser(null);
      setIsAuthenticated(false);
    }
  };

  const LogOut = async () => {
    setIsAuthenticated(false);
    localStorage.removeItem("accesstoken");
    await AuthService.LogOut();
    navigate("/");
  };

  const handleLogin = async ({ email, password }) => {
    debugger;
    setLoading(true);
    const LoginRequest = { Email: email, Password: password };
    try {
      const response = await AuthService.Login(LoginRequest);
      if (response.status === 200) {
        const data = await response.json();
        setTokenAndUser(data.accessToken);
        navigate("/home");
        setLoading(false);
        return true;
      } else if (response.status === 401) {
        setIsAuthenticated(false);
        setLoading(false);
        return false;
      }
      
    } catch (error) {
      console.error("Login failed", error);
      navigate("/error");
      setLoading(false);
    }
  };

  const handleRegisterUser = async (newUser) => {
    setLoading(true);
    try {
      const response = await AuthService.CreateUser({ newUser });
      if (response.status === 200) {
        const data = await response.json();
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
      } else if (response.status === 200) {
        const data = await response.json();
        setTokenAndUser(data.accessToken);
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
        emailAllreadyExist,
      }}
    >
      {props.children}
    </AuthContext.Provider>
  );
};

export default AuthProvider;
