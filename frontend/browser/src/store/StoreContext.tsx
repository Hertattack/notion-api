import RootStore from "./RootStore.ts";
import React, {createContext, useContext} from "react";
import configuration from "../configuration.ts";

const rootStore = new RootStore(configuration.notionApi);
const StoreContext = createContext<RootStore>(rootStore);

interface StoreContextProperties {
    children?: React.ReactNode;
}

const StoreContextProvider : React.FC<StoreContextProperties> = ({children})=>{
    return (
        <StoreContext.Provider value={rootStore}>
            {children}
        </StoreContext.Provider>
    );
};

const useStore = () => useContext<RootStore>(StoreContext);

export {
    StoreContextProvider,
    useStore
};