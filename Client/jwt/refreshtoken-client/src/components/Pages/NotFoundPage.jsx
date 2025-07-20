import React from "react";
import { Link } from "react-router-dom";

const NotFoundPage = () => {
  return (
    <div className="d-flex flex-column justify-content-center align-items-center vh-100 bg-light">
      <h1 className="display-1 fw-bold text-danger">404</h1>
      <p className="fs-3">
        <span className="text-danger">Oj!</span> Sidan kunde inte hittas.
      </p>
      <p className="lead">Den sida du försökte nå finns inte.</p>
      <Link to="/" className="btn btn-primary">
        Till startsidan
      </Link>
    </div>
  );
};

export default NotFoundPage;