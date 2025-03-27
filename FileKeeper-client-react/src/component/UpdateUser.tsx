import { FormEvent, useContext, useState, useEffect } from "react";
import { Button, Dialog, DialogActions, DialogContent, DialogTitle, TextField } from "@mui/material";
import { UserContext } from "../context/userContext";
import axios from "axios";
import { cancelButtonStyles, registerButtonStyles } from "../styles/buttonStyles";

type UpdateUserProps = {
    open: boolean;
    onClose: () => void;
}

const UpdateUser = ({ open, onClose }: UpdateUserProps) => {
    const userDetail = useContext(UserContext);
    const [firstName, setName] = useState("");
    const [lastName, setLastName] = useState("");
    const [email, setEmail] = useState("");
    const [address, setAddress] = useState("");
    const [phone, setPhone] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState('');
    
    useEffect(() => {
        if (open) {
            setName(userDetail.user.firstName);
            setLastName(userDetail.user.lastName);
            setEmail(userDetail.user.email);
            setAddress(userDetail.user.address);
            setPhone(userDetail.user.phone);
            setPassword(userDetail.user.password);
        }
    }, [open, userDetail.user]);

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();
        try {
            const updatedData = {
                firstName, lastName, email, address, password, phone, id: userDetail.user.id
            };
            const response = await axios.put('http://localhost:3000/api/user', updatedData, {
                headers: {
                    'user-id': userDetail.user.id.toString()
                }
            });
            const successMessage = response.data.message;
            alert(successMessage); 
            userDetail.dispatch({
                type: 'UPDATE', data: {
                    id: userDetail.user.id, firstName, lastName, email, address, phone, password
                }
            });
            onClose();
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                const message = error.response.data.message;
                setError(message); 
            } else {
                setError("שגיאה בשרת, אנא נסה שוב מאוחר יותר");
            }
            console.error("Error updating user:", error);
        }
    };

    return (
        <Dialog open={open} onClose={onClose}>
            <DialogTitle>עדכון פרטי משתמש</DialogTitle>
            <DialogContent>
                <form onSubmit={handleSubmit}>
                    <TextField label="שם" value={firstName} onChange={(e) => setName(e.target.value)} fullWidth margin="normal" />
                    <TextField label="שם משפחה" value={lastName} onChange={(e) => setLastName(e.target.value)} fullWidth margin="normal" />
                    <TextField label="מייל" value={email} onChange={(e) => setEmail(e.target.value)} fullWidth margin="normal" />
                    <TextField label="כתובת" value={address} onChange={(e) => setAddress(e.target.value)} fullWidth margin="normal" />
                    <TextField label="טלפון" value={phone} onChange={(e) => setPhone(e.target.value)} fullWidth margin="normal" />
                    <TextField label="סיסמא" type="password" value={password} onChange={(e) => setPassword(e.target.value)} fullWidth margin="normal" />
                    {error && <div style={{ color: 'red' }}>{error}</div>}  
                    <DialogActions>
                        <Button type="submit"  color="primary" sx={registerButtonStyles}>שמירה</Button>
                        <Button onClick={onClose} color="secondary" sx={cancelButtonStyles}>ביטול</Button>
                    </DialogActions>
                </form>
            </DialogContent>
        </Dialog>
    );
};

export default UpdateUser;