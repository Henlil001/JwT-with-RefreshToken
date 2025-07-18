import { useState } from 'react'
import { Route, Routes } from "react-router-dom";
import HomePage from './components/Pages/HomePage';
import LoginPage from './components/Pages/LoginPage';
import AuthProvider from './context/AuthProvider';
import ErrorPage from './components/Pages/ErrorPage';
import ScrollToTop from './components/Features/ScrollToTop';

function App() {

  return (
    <>
      <AuthProvider>
        <ScrollToTop />
          <Routes>
            <Route exact path="/" element={<LoginPage />} />
            <Route path="/home" element={<HomePage />} />
            <Route path="*" element={<ErrorPage />} />
          </Routes>
      </AuthProvider>
    </>
  )
}

export default App
