import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import { UserProvider } from './context/userContext'
import Main from './component/Main'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
        <UserProvider>
            <Main />
        </UserProvider>
    </>
  )
}

export default App
