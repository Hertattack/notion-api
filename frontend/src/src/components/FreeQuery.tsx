import React from "react";

export const FreeQuery : React.FC = () => {
    function click() {
        alert('hi');
    }

    return <div>
        <input type={"text"} id={"queryString"}/><button type={"button"} onClick={click}>Click me</button>
  </div>
};