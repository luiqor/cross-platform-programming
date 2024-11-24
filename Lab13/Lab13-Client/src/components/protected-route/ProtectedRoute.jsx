import { Navigate } from "react-router-dom";
import { AppRoute } from "../../constants/constants";

const ProtectedRoute = ({ children }) => {
  const authToken = document.cookie
    .split("; ")
    .find((row) => row.startsWith("AuthToken="))
    ?.split("=")[1];

  return authToken ? children : <Navigate to={AppRoute.LOGIN} />;
};

export { ProtectedRoute };
