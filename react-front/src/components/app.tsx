import { useEffect, useState } from "react"
import Cookies from "js-cookie"
import Login from "./login"
import api from "../api/axios"
import TaskList from "./tasklist"

const App = () => {

    let [loggedIn, setLoggedIn] = useState(false)

    const fetchToken = () => {
        const token = Cookies.get('jwt')
        
        if (token) {
            api.defaults.headers.common = { 'Authorization': `Bearer ${token}` }
        }
        
        setLoggedIn(!!token)
    }

    useEffect(fetchToken, [])

    return <>
        { loggedIn ? <TaskList /> : <Login onLogin={fetchToken} /> }
    </>
}

export default App