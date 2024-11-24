import {
  createBrowserRouter,
  Navigate,
  RouterProvider,
  Outlet,
} from "react-router-dom";
import { AppRoute, LabRoute } from "./constants/constants";
import { Home, Login, Lab1, Register, Profile } from "./pages/pages";
import { Layout, ProtectedRoute } from "./components/components";

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
          element: <Login />,
          path: AppRoute.LOGIN,
        },
        {
          element: <Register />,
          path: AppRoute.REGISTER,
        },
        {
          element: (
            <ProtectedRoute>
              <Profile />
            </ProtectedRoute>
          ),
          path: AppRoute.PROFILE,
        },
        {
          element: (
            <ProtectedRoute>
              <Outlet />
            </ProtectedRoute>
          ),
          path: AppRoute.LAB,
          children: [
            {
              element: <Lab1 />,
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
          ],
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
