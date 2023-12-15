import { View, Text } from "react-native";
import Login from "./components/login";
import * as SecureStore from "expo-secure-store";
import { useEffect, useState } from "react";
import TaskList from "./components/tasklist";
import api from "./api";

const Home = () => {
    const [loggedIn, setLoggedIn] = useState(false)

    const checkAuth = async () => {
        const token = await SecureStore.getItemAsync('jwt_token')
        if (token) {
            api.defaults.headers.common = { 'Authorization': `Bearer ${token}` }
        }
        setLoggedIn(!!token)
    }

    useEffect(() => {
        checkAuth()
    }, [])    

    return (
        <View>
            {/* { loggedIn ? <Login /> : <TaskList />} */}
            { loggedIn ? <TaskList /> : <Login onLogin={() => checkAuth()}/>}
        </View>
    )
}

export default Home