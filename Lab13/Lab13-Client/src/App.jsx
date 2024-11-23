import {
  createBrowserRouter,
  Navigate,
  RouterProvider,
} from "react-router-dom";
import { AppRoute, LabRoute } from "./constants/constants";
import { Home } from "./pages/pages";
import { Layout } from "./components/components";

const App = () => {
 const router = createBrowserRouter([
  {
    path: "/",
    element: <Layout />,
    children: [
      {
        element: <Home />,
        path: AppRoute.ROOT,
      },
      {
        element: <div>Login</div>,
        path: AppRoute.LOGIN,
      },
      {
        element: <div>REGISTER</div>,
        path: AppRoute.REGISTER,
      },
      {
        element: <div>PROFILE</div>,
        path: AppRoute.PROFILE,
      },
      {
        path: AppRoute.LAB,
        children: [
          {
            element: <div>LAB1</div>,
            path: LabRoute.LAB1,
          },
          {
            element: <div>LAB2</div>,
            path: LabRoute.LAB2,
          },
          {
            element: <div>LAB3</div>,
            path: LabRoute.LAB3,
          },
        ]
      },
      {
        element: <Navigate to={AppRoute.ROOT} replace />,
        path: AppRoute.ANY,
      },
    ],
  },
]);

  return <RouterProvider router={router} />;
};

export default App;
