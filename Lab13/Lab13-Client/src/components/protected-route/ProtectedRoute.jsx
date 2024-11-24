import { Navigate } from "react-router-dom";
import { AppRoute } from "../../constants/constants";
import { getCookie } from "../../helpers/helpers";

const ProtectedRoute = ({ children }) => {
  const authToken = getCookie("AuthToken");

  return authToken ? children : <Navigate to={AppRoute.LOGIN} />;
};

export { ProtectedRoute };
