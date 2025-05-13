import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import { createRootRoute, Outlet } from "@tanstack/react-router";
import "./root.scss";

export const Route = createRootRoute({
  component: () => {
    return (
      <>
        <div>
          <header></header>
        </div>

        <div className="app">
          <Outlet />
        </div>
        <ReactQueryDevtools />
      </>
    );
  },
});
