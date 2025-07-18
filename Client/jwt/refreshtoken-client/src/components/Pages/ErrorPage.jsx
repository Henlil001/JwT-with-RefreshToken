// ErrorPage.js
import React from "react";
import { Link } from "react-router-dom"; // För att ge användaren möjlighet att gå tillbaka till startsidan

const ErrorPage = () => {
  return (
    <div style={styles.container}>
      <h1 style={styles.heading}>Oops! Något gick fel.</h1>
      <p style={styles.message}>
        Det verkar som om något inte fungerar som det ska.
      </p>
      <p style={styles.message}>
        Försök gärna igen senare, eller gå till{" "}
        <Link to="/" style={styles.link}>
          startsidan
        </Link>
        .
      </p>
    </div>
  );
};

// CSS-stilar för ErrorPage
const styles = {
  container: {
    textAlign: "center",
    padding: "50px",
    fontFamily: "Arial, sans-serif",
  },
  heading: {
    fontSize: "48px",
    color: "#f44336", // Röd färg för att indikera ett fel
  },
  message: {
    fontSize: "18px",
    marginBottom: "20px",
  },
  link: {
    color: "#2196F3", // Blå färg för länken
    textDecoration: "none",
  },
};

export default ErrorPage;