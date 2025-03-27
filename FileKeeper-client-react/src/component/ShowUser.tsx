import { Menu, MenuItem, IconButton, Avatar, Typography, Box } from '@mui/material';
import { useContext, useState } from 'react';
import { UserContext } from '../context/userContext';

type UserMenuProps = {
    onUpdateClick: () => void;
    onLogout: () => void;
}

const ShowUser = ({ onUpdateClick, onLogout }: UserMenuProps) => {
    const { user } = useContext(UserContext);
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);

    const handleMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };

    return (
        <div>
           <Box display="flex" alignItems="center">
            <IconButton color="inherit" onClick={handleMenu}>
                <Avatar sx={{ bgcolor: '#f5f5dc', color: '#000' }}>{user.firstName.charAt(0)}</Avatar>
            </IconButton>
            <Typography variant="h6" sx={{ marginLeft: 1, fontFamily: 'Arial, sans-serif', fontSize: '1.2rem', color: '#333' }}>
                hello {user.firstName}
            </Typography>
        </Box>
            <Menu
                anchorEl={anchorEl}
                open={Boolean(anchorEl)}
                onClose={handleClose}
            >
                <MenuItem onClick={() => { handleClose(); onUpdateClick(); }}>Update</MenuItem>
                <MenuItem onClick={() => { handleClose(); onLogout(); }}>Logout</MenuItem>
            </Menu>
        </div>
    );
};

export default ShowUser;
