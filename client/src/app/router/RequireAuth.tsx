import React from "react";
import { useAccount } from "../../lib/hooks/useAccount";
import { Navigate, Outlet, useLocation } from "react-router-dom";
import { Typography } from "@mui/material";

function RequireAuth() {
  const { currentUser, loadingUserInfo } = useAccount();
  const location = useLocation();

  if (loadingUserInfo) return <Typography>Loading...</Typography>;
  if (!currentUser) return <Navigate to="/login" state={{ from: location }} />;
  return <Outlet />;
}

export default RequireAuth;
