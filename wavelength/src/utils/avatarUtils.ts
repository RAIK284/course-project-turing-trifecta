// dictionary to link a uuid to the avatar png image
import Avatar1 from '../assets/avatars/avatar1.png';
import Avatar2 from '../assets/avatars/avatar2.png';
import Avatar3 from '../assets/avatars/avatar3.png';
import Avatar4 from '../assets/avatars/avatar4.png';
import Avatar5 from '../assets/avatars/avatar5.png';
import Avatar6 from '../assets/avatars/avatar6.png';


export const avatarDict: {
    [key: string]: string
} = {
    "8712a062-2c41-4111-a062-835c1adcd66d": Avatar1,
    "8712a062-2c41-4111-a062-835c1adcd66e": Avatar2,
    "8712a062-2c41-4111-a062-835c1adcd66f": Avatar3,
    "8712a062-2c41-4111-a062-835c1adcd670": Avatar4,
    "8712a062-2c41-4111-a062-835c1adcd671": Avatar5,
    "8712a062-2c41-4111-a062-835c1adcd672": Avatar6
}

// function to get a random avatar for when a user creates their account
export const getRandomAvatar = () => {
    const keys = Object.keys(avatarDict);
    return keys[Math.floor(Math.random() * keys.length)];
}