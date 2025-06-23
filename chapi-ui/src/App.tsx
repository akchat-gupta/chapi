import "./App.css";
import { SwaggerChatAgent } from "./components/SwaggerChatAgent";

function App() {
  return (
    <main className="bg-gray-50 min-h-screen">
      <h1 className="text-2xl font-bold text-center py-6">LLM Chat Agent</h1>
      <SwaggerChatAgent />
    </main>
  );
}

export default App;
