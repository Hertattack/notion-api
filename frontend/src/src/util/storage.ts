import React, {useEffect, useState} from "react";


function useLocalStorage<TDataType>(storageKey : string, fallbackState : TDataType ) :  [TDataType, React.Dispatch<React.SetStateAction<TDataType>>] {
    // https://www.robinwieruch.de/local-storage-react/

    let storedValue = localStorage.getItem(storageKey);
    let defaultValue = storedValue ? JSON.parse(storedValue) as TDataType : fallbackState;

    const [value, setValue] = useState<TDataType>(defaultValue);

    useEffect(() => {
        localStorage.setItem(storageKey, JSON.stringify(value));
    }, [value, storageKey]);

    return [value, setValue];
}

export {
    useLocalStorage
};
