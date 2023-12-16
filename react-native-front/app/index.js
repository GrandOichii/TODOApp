import { TouchableOpacity, View, Text } from "react-native";
import Login from "./components/login";
import * as SecureStore from "expo-secure-store";
import { useEffect, useState } from "react";
import TaskList from "./components/tasklist";
import { SafeAreaView } from "react-native-safe-area-context";

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


    return (
        <SafeAreaView>
            { loggedIn ? <TaskList checkAuth={checkAuth} /> : <Login onLogin={() => checkAuth()}/> }
        </SafeAreaView>
    )
}

export default Home