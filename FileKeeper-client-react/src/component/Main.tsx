import { AppBar, Toolbar, Button, Box, Divider } from '@mui/material';
import { BrowserRouter as Router, Link } from 'react-router-dom';
import { useContext, useState } from 'react';
import { UserContext } from "../context/userContext";
import ShowUser from './ShowUser';
import LoginDialog from './LoginDialog';
import UpdateUser from './UpdateUser';
import AppRoutes from './AppRoutes';

const Main = () => {
    const { user } = useContext(UserContext);
    const [loginOpen, setLoginOpen] = useState(false);
    const [updateOpen, setUpdateOpen] = useState(false);
    return (
        <Router>
            <AppBar position="static" sx={{ backgroundColor: '#A52A2A' }}>
                <Toolbar sx={{ justifyContent: 'space-between' }}>
                    <Box sx={{ display: 'flex', alignItems: 'center' }}>
                        {!user.email ? (
                            <Button color="inherit" onClick={() => setLoginOpen(true)}>התחבר</Button>
                        ) : (
                            <ShowUser onUpdateClick={() => setUpdateOpen(true)} onLogout={() => { }} />
                        )}
                    </Box>
                    <Box sx={{ display: 'flex', alignItems: 'center' }}>
                        <Button color="inherit" component={Link} to="/">עמוד הבית</Button>
                        <Divider orientation="vertical" flexItem sx={{ backgroundColor: 'white', margin: '0 10px' }} />

                        <Button color="inherit" component={Link} to="/about">אודות</Button>
                        <Divider orientation="vertical" flexItem sx={{ backgroundColor: 'white', margin: '0 10px' }} />
                        
                        <Button color="inherit" component={Link} to="/recipes">הצג מתכונים</Button>
                        {user.email && (
                            <>
                            <Divider orientation="vertical" flexItem sx={{ backgroundColor: 'white', margin: '0 10px' }} />
                            </>
                        )}
                    </Box>
                </Toolbar>
            </AppBar>

            <AppRoutes />

            <LoginDialog open={loginOpen} onClose={() => setLoginOpen(false)} />
            <UpdateUser open={updateOpen} onClose={() => setUpdateOpen(false)} />

        </Router>
    );
};

export default Main;
