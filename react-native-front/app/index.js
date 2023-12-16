import { TouchableOpacity, View, Text } from "react-native";
import Login from "./components/login";
import * as SecureStore from "expo-secure-store";
import { useEffect, useState } from "react";
import TaskList from "./components/tasklist";
import api from "./api";

const Home = () => {
    const [loggedIn, setLoggedIn] = useState(false)
    
    useEffect(() => {
        checkAuth()
    }, [])

    const checkAuth = async () => {
        const token = await SecureStore.getItemAsync('jwt_token')
        if (token) {
            api.defaults.headers.common = { 'Authorization': `Bearer ${token}` }
        }
        setLoggedIn(!!token)
    }

    const handlePress = async () => {
        await SecureStore.deleteItemAsync('jwt_token')
        checkAuth()
    }

    return (
        <View>
            {/* { loggedIn ? <Login /> : <TaskList />} */}
            { loggedIn ? <TaskList /> : <Login onLogin={() => checkAuth()}/>}
            <TouchableOpacity onPress={handlePress}>
                <Text>Reset JWT</Text>
            </TouchableOpacity>

        </View>
    )
}

export default Home