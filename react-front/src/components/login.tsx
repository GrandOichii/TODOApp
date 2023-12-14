import { ComponentProps, FormEvent, useState } from "react"
import api from "../api/axios"
import Cookies from "js-cookie"

interface LoginProps extends ComponentProps<"div"> {
    onLogin: () => void
}

const Login = (props: LoginProps) => {
    // const usernameRef = useRef(null);

    const [failed, setFailed] = useState(false)

    const submit = async (e: FormEvent) => {
        e.preventDefault()
        const target = e.target as typeof e.target & {
            username: { value: string },
            password: { value: string },
        }
        const uname = target.username.value
        const pass = target.password.value

        try {
            
            const resp = await api.post('/api/users/login', {
                username: uname,
                password: pass
            })
            
            const token = resp.data as string
            Cookies.set('jwt', token)
            props.onLogin()
        } catch (e) {
            setFailed(true)            
        }
    }

    return <>
        <div style={{display: "grid", placeItems: "center"}}> 
            <form onSubmit={submit}>
                <div style={{padding: 8}}>
                    <label>Username: </label>
                    <input type="text" name="username" required />
                </div>
                <div style={{padding: 8}}>
                    <label>Password: </label>
                    <input type="password" name="password" required />
                </div>
                <div style={{padding: 8}}>
                    <input type="submit" value="Login" />
                    { failed && <label style={{color: 'red', marginLeft: 5}}>Failed to log in</label> }
                </div>
            </form>
        </div>
    </>
}

export default Login