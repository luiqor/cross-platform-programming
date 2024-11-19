import {
  createBrowserRouter,
  Navigate,
  RouterProvider,
} from "react-router-dom";
import { AppRoute } from "./constants/constants";
import { Home } from "./pages/pages";

const App = () => {
  const router = createBrowserRouter([
    {
      element: <Home />,
      path: AppRoute.ROOT,
    },
    {
      element: <Navigate to={AppRoute.ROOT} replace />,
      path: AppRoute.ANY,
    },
  ]);

  return <RouterProvider router={router} />;
};

export default App;
