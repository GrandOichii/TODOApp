import { useRef, useState } from "react";
import { View, Text, TextInput, TouchableOpacity } from "react-native";

import api from '../api'
import { setStored } from "../storage";

const TextInputStyle = { 
    borderColor: 'black', 
    borderWidth: 1 
}

const Login = (props) => {

    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [failedMessage, setFailedMessage] = useState('')
    const [isProcessing, setIsProcessing] = useState(false)

    const handleLogin = async () => {
        try {
            setIsProcessing(true)
            const resp = await api.post('/api/users/login', {
                username: username, 
                password: password
            })
            setFailedMessage('')
            await setStored('jwt_token', resp.data)
            props.onLogin()
        } catch (e) {
            // throw e
            setFailedMessage('Failed to log in')
        }
        setIsProcessing(false)
    }
    
    const handleRegister = async () => {
        try {
            setIsProcessing(true)
            const data = {
                username: username, 
                password: password

            }
            await api.post('/api/users/register', data)
        } catch (e) {
            setIsProcessing(false)
            setFailedMessage('Failed to register')
            return
        }
        await handleLogin()
    }

    return <View>
        <TextInput onChangeText={value => setUsername(value)} placeholder="Username" style={TextInputStyle} />
        <TextInput onChangeText={value => setPassword(value)} placeholder="Password" style={TextInputStyle} />
        <View style={{justifyContent: 'space-around', flexDirection: 'row'}}>
            <TouchableOpacity disabled={isProcessing} onPress={handleLogin}>
                <Text>Login</Text>
            </TouchableOpacity>
            <TouchableOpacity disabled={isProcessing} onPress={handleRegister}>
                <Text>Register</Text>
            </TouchableOpacity>
        </View>
        { failedMessage !== '' && <Text style={{color: 'red'}}>{failedMessage}</Text>}
    </View>
}

export default Login