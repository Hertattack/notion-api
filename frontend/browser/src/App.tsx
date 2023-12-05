import './App.css';
//import {MetadataBrowser} from "./components/MetadataBrowser.tsx";
import {Graph} from "./features/graph";
import {NodeSpecification} from "./features/graph/NodeSpecification.ts";

function App() {
    const nodes : NodeSpecification[] = [
        {id: "a", label: "Aaaa"},
        {id: "b", label: "Bbbb"}
    ];


  return (
    <>
        <Graph id="metadata" nodes={nodes}/>
    </>
  )
}


export default App
