import './App.css';
import Xarrow, {Xwrapper} from "react-xarrows";
import {DraggableBox} from "./components/DraggableBox.tsx";

function App() {

  return (
    <>
        <Xwrapper>
            <div>
            <DraggableBox id={'elem1'}/>
            <br/>
            <br/>
            <br/>
            <DraggableBox id={'elem2'}/>
            </div>
            <Xarrow start={'elem1'} end="elem2" strokeWidth={1}/>
        </Xwrapper>
    </>
  )
}


export default App
