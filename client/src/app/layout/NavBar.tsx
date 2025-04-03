import {
  Container,
  MenuItem,
  AppBar,
  Box,
  Toolbar,
  Typography,
} from "@mui/material";
import { Group } from "@mui/icons-material";
import { NavLink } from "react-router";
import MenuItemLink from "../shared/components/MenuItemLink";
import { useAccount } from "../../lib/hooks/useAccount";
import UserMenu from "./UserMenu";

export default function NavBar() {
  const { currentUser } = useAccount();
  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar
        position="static"
        sx={{
          backgroundImage:
            "linear-gradient(135deg,#182a73 0%, #218aae 69%, #20a7ac 89%)",
        }}
      >
        <Container maxWidth="xl">
          <Toolbar sx={{ display: "flex", justifyContent: "space-between" }}>
            <Box>
              <MenuItem
                component={NavLink}
                to="/"
                sx={{ display: "flex", gap: 2 }}
              >
                <Group fontSize="large" />
                <Typography variant="h4" fontWeight="bold">
                  Reactivities
                </Typography>
              </MenuItem>
            </Box>
            <Box sx={{ display: "flex" }}>
              <MenuItemLink to="/activities">Activities</MenuItemLink>
            </Box>
            <Box display="flex" alignItems="center">
              {currentUser ? (
                <UserMenu />
              ) : (
                <>
                  <MenuItemLink to="/login">Login</MenuItemLink>
                  <MenuItemLink to="/register">Register</MenuItemLink>
                </>
              )}
            </Box>
          </Toolbar>
        </Container>
      </AppBar>
    </Box>
  );
}
