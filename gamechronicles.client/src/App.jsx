import { Route, Routes, useLocation } from "react-router-dom";
import "./App.css";

import { ListCustomers } from "./Components/ListCustomers.jsx";
import CreateCustomer from "./Components/CreateCustomer.jsx";
import UpdateCustomer from "./Components/UpdateCustomer.jsx";

function App() {
  const location = useLocation();
  return (
    <>
        <Routes location={location} key={location.pathname}>
        <Route path="/" element={<ListCustomers/>} />
        <Route path="/NewCustomer" element={<CreateCustomer/>} />
        <Route path="/UpdateCustomer/:idCustomer" element={<UpdateCustomer/>} />
      </Routes>
    </>
  );
}

export default App;
