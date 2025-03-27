import { createContext, Dispatch, ReactNode, useReducer } from "react";

export type userType = {
    id:number,
    firstName: string,
    lastName: string,
    email: string,
    address: string,
    password: string,
    phone: string
}

export type Action = { 
    type: 'UPDATE',
    data: userType
} | {
    type: 'DELETE',
} | {
    type: 'CREATE',
    id:number,
    email: string,
    password: string 
}

const userReducer = (state: userType, action: Action): userType => {
    switch (action.type) {
        case 'UPDATE':
            return { ...state, ...action.data };
        case 'DELETE':
            return { ...state };
        case 'CREATE':
            return { ...state, id:action.id,email: action.email, password: action.password };    
        default: 
            return state;
    }
};

const initialState: userType = {id:1, firstName: '', email: '', lastName: '', phone: '', password: '', address: '' };
export const UserContext = createContext<{ user: userType; dispatch: Dispatch<Action> }>({ user: initialState, dispatch: () => null });

export const UserProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [user, dispatch] = useReducer(userReducer, initialState);
    return (
        <UserContext.Provider value={{ user, dispatch }}>
            {children}
        </UserContext.Provider>
    );
};
