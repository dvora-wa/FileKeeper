import { useContext, useState } from "react";
import { Button, Dialog, DialogActions, DialogContent, DialogTitle, TextField } from "@mui/material";
import { UserContext } from "../context/userContext";
import axios from 'axios'; 
import { cancelButtonStyles, loginButtonStyles, registerButtonStyles } from '../styles/buttonStyles';
import { UseStateProps } from "../Types/type";

const LoginDialog = ({ open, onClose }: UseStateProps) => {
    const { dispatch } = useContext(UserContext);
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const handleLogin = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        try {
            const response = await axios.post('http://localhost:3000/api/user/login', { email, password });
            dispatch({
                type: 'UPDATE', 
                data: {
                    id: response.data.user.id || "",
                    firstName: response.data.user.firstName || "", 
                    lastName: response.data.user.lastName || "", 
                    email: response.data.user.email || "", 
                    address: response.data.user.address || "",
                    phone: response.data.user.phone || "", 
                    password: response.data.user.password || ""
                }
            });
            alert(response.data.message);
            onClose(false);
        } catch (err) {
            if (axios.isAxiosError(err) && err.response) {
                setError(err.response.data.message);
            } else {
                setError("שגיאה בשרת, אנא נסה שוב מאוחר יותר");
            }
        }
    };
    const handleRegister = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        try {
            const response = await axios.post('http://localhost:3000/api/user/register', { email, password });
            dispatch({ type: 'CREATE', id: response.data.userId, email: email, password: password });
            onClose(false);
            alert(response.data.message);
        } catch (err) {
            if (axios.isAxiosError(err) && err.response) {
                setError(err.response.data.message);
            } else {
                setError("שגיאה בשרת, אנא נסה שוב מאוחר יותר");
            }
        }
    };
    return (
        <>
        <Dialog open={open} onClose={() => onClose(false)}>
            <DialogTitle>Login</DialogTitle>
            <DialogContent sx={{ display: 'flex', flexDirection: 'column', gap: 2, backgroundColor: '#f0f0f0' }}>
                <form onSubmit={handleLogin}>
                    <TextField 
                        label="Email" 
                        value={email} 
                        onChange={(e) => setEmail(e.target.value)} 
                        fullWidth 
                        sx={{ marginBottom: 2 }} 
                    />
                    <TextField 
                        label="Password" 
                        type="password" 
                        value={password} 
                        onChange={(e) => setPassword(e.target.value)} 
                        fullWidth 
                        sx={{ marginBottom: 2 }} 
                    />
                    {error && <div style={{ color: 'red' }}>{error}</div>}
                    <DialogActions>
                        <Button onClick={() => onClose(false)} size="small" sx={cancelButtonStyles}>
                            Cancel
                        </Button>
                        <Button type="submit" size="small" sx={loginButtonStyles}>
                            Login
                        </Button>
                        <Button onClick={handleRegister} size="small" sx={registerButtonStyles}>
                            Register
                        </Button>
                    </DialogActions>
                </form>
            </DialogContent>
        </Dialog>
        </>
    );
};

export default LoginDialog;
