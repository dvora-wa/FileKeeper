import { Box, Typography } from "@mui/material";
import homeImage from '../images/home.jpg';

const HomePage = () => {
    return (
        <Box sx={{ position: 'relative', height: `calc(100vh - 80px)`, overflow: 'hidden' }}>
            <Box 
                component="img" 
                src={homeImage} 
                alt="תמונה לעמוד הבית" 
                sx={{ 
                    width: '100%', 
                    height: '100%', 
                    position: 'absolute', 
                    top: 0,
                    left: 0,
                    zIndex: -1 
                }} 
            />
            <Typography variant="h2" sx={{ position: 'relative', zIndex: 1, color: 'white', textAlign: 'center', paddingTop: '20px' }}>
               Home Page
            </Typography>
        </Box>
    );
};

export default HomePage;
