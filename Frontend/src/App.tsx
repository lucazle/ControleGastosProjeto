import Pessoas from "./components/Pessoas";
import Transacoes from "./components/Transacoes";
import Relatorio from "./components/Relatorio"
import "./App.css";

function App() {
  return (
    <div>
      <h1>Controle de Gastos</h1>
      <Pessoas />
      <Transacoes />
      <Relatorio />
    </div>
  );
}

export default App;