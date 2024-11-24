import { Outlet } from 'react-router-dom';

const Layout = () => {
  return (
    <main style={{ width: "100vw" }}>
        <Outlet />
    </main>
  );
};

export { Layout };