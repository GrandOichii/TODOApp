import { useRef, useState } from "react";
import { View, Text, TextInput, TouchableOpacity } from "react-native";
import * as SecureStore from "expo-secure-store";

import api from '../api'

const TextInputStyle = { 
    borderColor: 'black', 
    borderWidth: 1 
}

const Login = (props) => {

    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [failedMessage, setFailedMessage] = useState('')

    const handleLogin = async () => {
        try {
            const resp = await api.post('/api/users/login', {
                username: username, 
                password: password
            })
            setFailedMessage('')
            await SecureStore.setItemAsync('jwt_token', resp.data)
            props.onLogin()
        } catch (e) {
            // throw e
            setFailedMessage('Failed to log in')
        }
    }
    
    const handleRegister = async () => {
        try {
            const data = {
                username: username, 
                password: password

            }
            await api.post('/api/users/register', data)
        } catch (e) {
            console.log(e);
            setFailedMessage('Failed to register')
            return
        }
        await handleLogin()
    }

    return <View>
        <TextInput onChangeText={value => setUsername(value)} placeholder="Username" style={TextInputStyle} />
        <TextInput onChangeText={value => setPassword(value)} placeholder="Password" style={TextInputStyle} />
        <View style={{justifyContent: 'space-around', flexDirection: 'row'}}>
            <TouchableOpacity onPress={handleLogin}>
                <Text>Login</Text>
            </TouchableOpacity>
            <TouchableOpacity onPress={handleRegister}>
                <Text>Register</Text>
            </TouchableOpacity>
        </View>
        { failedMessage !== '' && <Text style={{color: 'red'}}>{failedMessage}</Text>}
    </View>
}

export default Login