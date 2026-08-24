import react from '@vitejs/plugin-react'
import { defineConfig } from 'vite'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    // The API's dev CORS policy allows http://localhost:5173 only, so a different
    // port loads the app but breaks every request. Fail loudly instead.
    port: 5173,
    strictPort: true,
  },
})
