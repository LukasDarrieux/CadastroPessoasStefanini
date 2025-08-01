import "bootstrap/dist/js/bootstrap.bundle.min"
import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap-icons/font/bootstrap-icons.css";
import { StrictMode } from "react"
import { createRoot } from "react-dom/client"
import AppRoutes from "./components/AppRoutes.jsx"

createRoot(document.getElementById("root")).render(
    <StrictMode>
        <AppRoutes />
    </StrictMode>,
)
