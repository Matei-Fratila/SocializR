// import { AccountCircle, Search } from "@mui/icons-material";
// import { AppBar, Badge, Box, IconButton, Toolbar, Typography } from "@mui/material";

// const Nav = () => {
//     return (
//         <Box sx={{ flexGrow: 1 }}>
//             <AppBar position="static">
//                 <Toolbar>
//                     <IconButton
//                         size="large"
//                         edge="start"
//                         color="inherit"
//                         aria-label="open drawer"
//                         sx={{ mr: 2 }}
//                     >
//                         <MenuIcon />
//                     </IconButton>
//                     <Typography
//                         variant="h6"
//                         noWrap
//                         component="div"
//                         sx={{ display: { xs: 'none', sm: 'block' } }}
//                     >
//                         MUI
//                     </Typography>
//                     <Search>
//                         <SearchIconWrapper>
//                             <SearchIcon />
//                         </SearchIconWrapper>
//                         <StyledInputBase
//                             placeholder="Search…"
//                             inputProps={{ 'aria-label': 'search' }}
//                         />
//                     </Search>
//                     <Box sx={{ flexGrow: 1 }} />
//                     <Box sx={{ display: { xs: 'none', md: 'flex' } }}>
//                         <IconButton size="large" aria-label="show 4 new mails" color="inherit">
//                             <Badge badgeContent={4} color="error">
//                                 <MailIcon />
//                             </Badge>
//                         </IconButton>
//                         <IconButton
//                             size="large"
//                             aria-label="show 17 new notifications"
//                             color="inherit"
//                         >
//                             <Badge badgeContent={17} color="error">
//                                 <NotificationsIcon />
//                             </Badge>
//                         </IconButton>
//                         <IconButton
//                             size="large"
//                             edge="end"
//                             aria-label="account of current user"
//                             aria-controls={menuId}
//                             aria-haspopup="true"
//                             onClick={handleProfileMenuOpen}
//                             color="inherit"
//                         >
//                             <AccountCircle />
//                         </IconButton>
//                     </Box>
//                     <Box sx={{ display: { xs: 'flex', md: 'none' } }}>
//                         <IconButton
//                             size="large"
//                             aria-label="show more"
//                             aria-controls={mobileMenuId}
//                             aria-haspopup="true"
//                             onClick={handleMobileMenuOpen}
//                             color="inherit"
//                         >
//                             <MoreIcon />
//                         </IconButton>
//                     </Box>
//                 </Toolbar>
//             </AppBar>
//             {renderMobileMenu}
//             {renderMenu}
//         </Box>);
// };

// export default Nav;