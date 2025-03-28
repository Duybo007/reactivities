import NavBar from "./NavBar";
import { Box, Container, CssBaseline } from "@mui/material";
import { Outlet, useLocation } from "react-router";
import "./index.css";
import HomePage from "../../features/home/HomePage";

function App() {
  const location = useLocation();

  return (
    <Box sx={{ bgcolor: "#eeeeee", minHeight: "100vh" }}>
      <CssBaseline />
      {location.pathname === "/" ? (
        <HomePage />
      ) : (
        <>
          <NavBar />
          <Container maxWidth="xl" sx={{ mt: 3 }}>
            <Outlet />
          </Container>
        </>
      )}
    </Box>
  );
}

export default App;
