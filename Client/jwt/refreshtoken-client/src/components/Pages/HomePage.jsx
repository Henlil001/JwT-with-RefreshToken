import Header from "../../components/Headers/Header/Header";

const HomePage = () => {
  return (
    <>
      <Header
        //imageUrl="https://www.example.com/your-logo.jpg"
        title="Övningsbank"
      />
      <div className="text-alaign-center">
        <h1>Startsida</h1>
        <p>Du är inloggad</p>
      </div>
    </>
  );
};

export default HomePage;